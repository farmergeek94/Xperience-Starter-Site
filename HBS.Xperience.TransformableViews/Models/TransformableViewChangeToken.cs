using CMS.Core;
using CMS.Helpers;
using HBS.TransformableViews;
using HBS.Xperience.TransformableViews.Library;
using HBS.Xperience.TransformableViews.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBS.Xperience.TransformableViews.Models
{
    internal class TransformableViewChangeToken : IChangeToken
    {
        private readonly string _viewName;

        private ITransformableViewRepository _repository => Service.Resolve<ITransformableViewRepository>();

        public TransformableViewChangeToken(string viewName)
        {
            _viewName = viewName;
        }

        public bool ActiveChangeCallbacks => false;

        public bool HasChanged
        {
            get
            {
                try
                {
                    var names = _repository.GetTransformableViewNames();
                    if(!names.Any(x=>x == _viewName))
                    {
                        return false;
                    }
                    var view = _repository.GetTransformableView(_viewName);
                    if(view != null)
                    {
                        if(view.TransformableViewLastRequested == DateTimeHelper.ZERO_TIME)
                        {
                            return false;
                        }
                        return view.TransformableViewLastModified > view.TransformableViewLastRequested;
                    }
                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public IDisposable RegisterChangeCallback(Action<object> callback, object state) => EmptyDisposable.Instance;

        internal class EmptyDisposable : IDisposable
        {
            public static EmptyDisposable Instance { get; } = new EmptyDisposable();
            private EmptyDisposable() { }
            public void Dispose() { }
        }
    }
}

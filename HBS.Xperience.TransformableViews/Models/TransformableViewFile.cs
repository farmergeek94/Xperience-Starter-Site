using CMS.Core;
using CMS.Helpers;
using HBS.TransformableViews;
using HBS.Xperience.TransformableViews.Library;
using HBS.Xperience.TransformableViews.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xperience.Community.TransformableViews.Models
{
    internal class TransformableViewFile : IFileInfo
    {
        private string _viewPath = "";

        private ITransformableViewRepository _repository => Service.Resolve<ITransformableViewRepository>();

        private IProgressiveCache _progressiveCache => Service.Resolve<IProgressiveCache>();
        private byte[] _viewContent = Array.Empty<byte>();
        private DateTimeOffset _lastModified = DateTime.MinValue;
        private bool _exists = false;

        public TransformableViewFile(string viewName)
        {
            _viewPath = viewName;
            GetView(viewName);
        }
        public bool Exists => _exists;

        public bool IsDirectory => false;

        public DateTimeOffset LastModified => _lastModified;

        public long Length
        {
            get
            {
                using (var stream = new MemoryStream(_viewContent))
                {
                    return stream.Length;
                }
            }
        }

        public string Name => Path.GetFileName(_viewPath);

        public string PhysicalPath => null;

        public Stream CreateReadStream()
        {
            return new MemoryStream(_viewContent);
        }

        private void GetView(string viewName)
        {
            
            try
            {
                var view = _repository.GetTransformableView(viewName, true);
                if (view != null) {
                    _viewContent = Encoding.UTF8.GetBytes(view.TransformableViewContent);
                    _lastModified = view.TransformableViewLastModified;
                    _exists = true;
                }
            }
            catch (Exception ex)
            {
                // if something went wrong, Exists will be false
            }
        }
    }
}
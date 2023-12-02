using CMS.Helpers;
using HBS.TransformableViews;
using HBS.Xperience.TransformableViews.Models;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xperience.Community.TransformableViews.Models;

namespace HBS.Xperience.TransformableViews.Repositories
{
    public class TransformableViewFileProvider : IFileProvider
    {
        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            throw new NotImplementedException();
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            return new TransformableViewFile(subpath);
        }

        public IChangeToken Watch(string filter)
        {
            return new TransformableViewChangeToken(filter);
        }
    }
}

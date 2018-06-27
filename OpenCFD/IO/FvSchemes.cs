using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HopeCFD.OpenCFD.Dictionary;
using HopeCFD.OpenCFD.Schemes;
namespace HopeCFD.OpenCFD.IO
{
    [Serializable]
    public class FvSchemes:FoamFile
    {
        DictEntry _ddtSchemes;
        DictEntry _gradSchemes;
        DictEntry _divSchemes;
        DictEntry _laplacianSchemes;
        DictEntry _interpolationSchemes;
        DictEntry _snGradSchemes;

        public FvSchemes()
        {
            _ddtSchemes = DictEntry.Root("ddtSchemes");
            _gradSchemes = DictEntry.Root("gradSchemes");
            _divSchemes = DictEntry.Root("divSchemes");
            _laplacianSchemes = DictEntry.Root("laplacianSchemes");
            _interpolationSchemes = DictEntry.Root("interpolationSchemes");
            _snGradSchemes = DictEntry.Root("snGradSchemes");
        }
        public void AddDdtSchemes(string key, Ddt ddt)
        {
            _ddtSchemes.NewChild(key, ddt);
        }
        public void AddDdtSchemes(Ddt ddt)
        {
            AddDdtSchemes("default", ddt);
        }
        public void RemoveDdtSchemes(string key)
        {
            _ddtSchemes.RemoveChild(key);
        }
        public Ddt GetDdtSchemes(string key)
        {
            DictEntry e = _ddtSchemes.Lookup(key);
            if (e != null)
                return (Ddt)e.Item;
            else
                return null;
        }
        public void AddGradSchemes(string key, Grad s)
        {
            _gradSchemes.NewChild(key, s);
        }
        public void AddGradSchemes(Grad s)
        {
            AddGradSchemes("default",s);
        }
        public void RemoveGradSchemes(string key)
        {
            _gradSchemes.RemoveChild(key);
        }
        public Grad GetGradSchemes(string key)
        {
            DictEntry e = _ddtSchemes.Lookup(key);
            if (e != null)
                return (Grad)e.Item;
            else
                return null;
        }

        public void AddDivSchemes(string key, Div s)
        {
            _divSchemes.NewChild(key, s);
        }
        public void AddDivSchemes(Div s)
        {
            AddDivSchemes("default", s);
        }
        public void RemoveDivSchemes(string key)
        {
            _divSchemes.RemoveChild(key);
        }
        public Div GetDivSchemes(string key)
        {
            DictEntry e = _ddtSchemes.Lookup(key);
            if (e != null)
                return (Div)e.Item;
            else
                return null;
        }

        public void AddLaplacianSchemes(string key, Laplacian s)
        {
            _laplacianSchemes.NewChild(key, s);
        }

        public void AddLaplacianSchemes(Laplacian s)
        {
            AddLaplacianSchemes("default", s);
        }
        public void RemoveLaplacianSchemes(string key)
        {
            _laplacianSchemes.RemoveChild(key);
        }
        public Laplacian GetLaplacianSchemes(string key)
        {
            DictEntry e = _ddtSchemes.Lookup(key);
            if (e != null)
                return (Laplacian)e.Item;
            else
                return null;
        }
        public void AddInterpolationSchemes(string key, Interpolation s)
        {
            _interpolationSchemes.NewChild(key, s);
        }
        public void AddInterpolationSchemes(Interpolation s)
        {
            AddInterpolationSchemes("default", s);
        }
        public void RemoveInterpolationSchemes(string key)
        {
            _interpolationSchemes.RemoveChild(key);
        }
        public Interpolation GetInterpolationSchemes(string key)
        {
            DictEntry e = _ddtSchemes.Lookup(key);
            if (e != null)
                return (Interpolation)e.Item;
            else
                return null;
        }

        public void AddSnGradSchemes(string key, SnGrad s)
        {
            _snGradSchemes.NewChild(key, s);
        }
        public void AddSnGradSchemes(SnGrad s)
        {
            AddSnGradSchemes("default", s);
        }
        public void RemoveSnGradSchemes(string key)
        {
            _snGradSchemes.RemoveChild(key);
        }
        public SnGrad GetSnGradSchemes(string key)
        {
            DictEntry e = _ddtSchemes.Lookup(key);
            if (e != null)
                return (SnGrad)e.Item;
            else
                return null;
        }

        public override void Write(string root)
        {
            this.ClearContent();
            this.AddContent(_ddtSchemes);
            this.AddContent(_gradSchemes);
            this.AddContent(_divSchemes);
            this.AddContent(_laplacianSchemes);
            this.AddContent(_interpolationSchemes);
            this.AddContent(_snGradSchemes);
            this.header = FoamFileHeader.FvSchemesHeader;
            base.Write(root);
        }
    }
}

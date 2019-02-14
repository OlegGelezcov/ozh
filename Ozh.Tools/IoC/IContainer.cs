using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ozh.Tools.IoC
{
    public interface IContainer
    {
        IObjectBuilder AddTransient<ITypeToResolve, TConcrete>();

        IObjectBuilder AddTransient<TConcrete>();

        IObjectBuilder AddSingleton<ITypeToResolve, TConcrete>();

        IObjectBuilder AddSingleton<TConcrete>();

        ITypeToResolve Resolve<ITypeToResolve>();
        ITypeToResolve Resolve<ITypeToResolve>(string id);

        void Build();

    }
}

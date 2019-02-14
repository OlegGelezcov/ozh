using Ozh.Tools.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace OzhConsole
{
    public class ContainerTests
    {
        public void Install() {
            IoCContainer container = new IoCContainer();
            container.AddSingleton<IServiceA, ServiceA>();
            container.AddSingleton<IServiceA, ServiceA>().WithId("seconda");
            container.AddSingleton<IServiceB, ServiceB>();
            container.AddSingleton<FromFabricClass>().AsLazy().WithFabric(() => new FromFabricClass("Hello World"));
            container.AddTransient<FromFabricClass>().AsLazy().WithFabric(() => new FromFabricClass("NOT HELLO FABRIC")).WithId("fab");
            container.AddTransient<ServiceC>().AsLazy();
            container.Build();

            ReadLine();
            ServiceC c = container.Resolve<ServiceC>();
            c.Test();

            WriteLine("===================");
            c = container.Resolve<ServiceC>();
            c.Test();
        }
    }

    public class FromFabricClass
    {
        private string _guid;
        public string Id {
            get {
                if (_guid == null) {
                    _guid = Guid.NewGuid().ToString();
                }
                return _guid;
            }
        }

        public FromFabricClass(string someString) {
            WriteLine("FromFabric cons: " + someString);
        }
    }

    public interface IService
    {
        string Name { get; }
        string Id { get; }
    }

    public interface IServiceA : IService
    {
        
    }

    public interface IServiceB : IService
    {
    }

    public class ServiceA : IServiceA
    {
        public string Name { get; set; }

        private string _guid;
        public string Id {
            get {
                if(_guid == null ) {
                    _guid = Guid.NewGuid().ToString();
                }
                return _guid;
            }
        }

        public ServiceA() {
            Name = "ServiceA";

        }
    }

    public class ServiceB : IServiceB
    {
        private string id;

        public string Id {
            get {
                if(id == null ) {
                    id = Guid.NewGuid().ToString();
                }
                return id;
            }
        }

        public string Name { get; set; }
        public IServiceA A { get; set; }

        public ServiceB(IServiceA servicea) {
            Name = "ServiceB";
            A = servicea;
            Console.WriteLine("constructor B");
        }

        public void InjectWithConstructor() {
            Console.WriteLine("inject with constructor");
            Console.WriteLine((A as ServiceA).Id);

        }
    }

    public class ServiceC
    {
        private string id;

        public string Id {
            get {
                if(id == null ) {
                    id = Guid.NewGuid().ToString();
                }
                return id;
            }
        }

        [Inject]
        private IServiceA privateA;

        [Inject]
        public IServiceB publicB;

        [Inject]
        private IServiceA ServA { get; set; }

        [Inject]
        public IServiceB ServB { get; private set; }

        [Id("seconda")]
        [Inject]
        IServiceA ServAId { get; set; }

        [Inject]
        FromFabricClass fromFabric { get; set; }

        private IServiceA fromConstructorA;
        private IServiceB fromConstructorB;

        private IServiceA withConstructA;
        private IServiceB withConstructB;

        private FromFabricClass fromFrabricID;


        public ServiceC(IServiceA sA, IServiceB sB ) {
            WriteLine("Cons() C");
            fromConstructorA = sA;
            fromConstructorB = sB;
            
        }

        public void Construct([Id("seconda")]IServiceA sa, IServiceB sb, [Id("fab")]FromFabricClass cc) {
            withConstructA = sa;
            withConstructB = sb;
            fromFrabricID = cc;

        }

        public void Test() {
            WriteLine($"Cons() A: {fromConstructorA.Id}");
            WriteLine($"Cons() B: {fromConstructorB.Id}");
            WriteLine($"CONSTRUCT A: {withConstructA.Id}");
            WriteLine($"CONSTRUCT B: {withConstructB.Id}");
            WriteLine($"FROM FABRIC WITH ID: {fromFrabricID.Id}");
            WriteLine($"Inject field A: {privateA.Id}");
            WriteLine($"Inject field B: {publicB.Id}");
            WriteLine($"Inject prop A: {ServA.Id}");
            WriteLine($"Inject prop B: {ServB.Id}");
            WriteLine($"Inject prop A(seconda): {ServAId.Id}");
            WriteLine($"From Fabric prop: {fromFabric.Id}");
        }
    }
}

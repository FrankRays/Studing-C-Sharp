using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace IOSErvice
{
    [ServiceContract]
    class HelloService
    {
        [OperationContract]
        string HelloWorld(string name)
        {
            return string.Format("Hello {0}", name);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // We did not separate contract from implementation.
            // Therefor service and contract are the same in this example.

            Type serviceType = typeof(HelloService);
            ServiceHost host = new ServiceHost(serviceType, new Uri[] { new Uri("http://localhost:8080/") });

            //Add behavior for our MEX endpoint
            ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
            behavior.HttpGetEnabled = true;
            host.Description.Behaviors.Add(behavior);
            //Create basicHttpBinding endpoint at http://localhost:8080/HelloService/
            host.AddServiceEndpoint(serviceType, new BasicHttpBinding(), "HelloService");
            //Add MEX endpoint at http://localhost:8080/MEX/
            host.AddServiceEndpoint(typeof(IMetadataExchange), new BasicHttpBinding(), "MEX");
            host.Open();
            Console.WriteLine("Service is ready, press any key to terminate.");
            Console.ReadKey();
            host.Close();
        }
    }
}

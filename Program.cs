using REST_Demo_CSharp.bbdn.rest.helpers;
using REST_Demo_CSharp.bbdn.rest.services;
using System;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace REST_Demo_CSharp
{
    public class MainClass
    {
        private static Token token = new Token();

        [Flags]
        private enum Operations
        {
            // The flag for Create is 0001.
            C = 0x01,
            // The flag for Read is 0010.
            R = 0x02,
            // The flag for Update is 0100.
            U = 0x04,
            // The flag for Delete is 1000.
            D = 0x08
        }

        private static Operations SetOperations(String crud)
        {

            char[] charCrud = crud.ToCharArray();
            Operations operations = 0;

            for (int i = 0; i < charCrud.Length; i++)
            {
                switch (charCrud[i])
                {
                    case ('C'):
                        operations |= Operations.C;
                        break;
                    case ('R'):
                        operations |= Operations.R;
                        break;
                    case ('U'):
                        operations |= Operations.U;
                        break;
                    case ('D'):
                        operations |= Operations.D;
                        break;
                }
            }

            return operations;
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("args.Length: " + args.Length);

            string crud = "";

            if (args.Length == 2)
            {
                crud = args[1].ToUpper();
            }
            else
            {
                crud = "CRUD";
            }

            Operations operations = SetOperations(crud);

            String apis = args[0];

            try
            {
                AsyncContext.Run(() => MainAsync(operations, apis));
            }
            catch (AggregateException agex)
            {
                Console.WriteLine("AggregateException: " + agex.Message + " " + agex.InnerException);
            }
        }

        static async Task MainAsync(Operations operations, string apis)
        {


            Authorizer authorizer = new Authorizer();

            Console.WriteLine("calling authorize()");

            token = await authorizer.Authorize();

            Console.WriteLine("MainAsync(): Token=" + token.ToString());

            switch (apis)
            {
                case ("--datasource"):
                case ("-d"):
                    Console.WriteLine("Datasource");
                    bool result = await DoDatasource(operations);
                    break;
                case ("--term"):
                case ("-t"):
                    result = await DoTerm(operations);
                    break;
                case ("--course"):
                case ("-c"):
                    result = await DoCourse(operations);
                    break;
                case ("--user"):
                case ("-u"):
                    result = await DoUser(operations);
                    break;
                case ("--membership"):
                case ("-m"):
                    result = await DoMembership(operations);
                    break;
                case ("--all"):
                default:
                    result = await DoAll();
                    break;

            }


        }

        private static async Task<bool> DoDatasource(Operations operations)
        {
            if (token.access_token == null)
            {
                Authorizer authorizer = new Authorizer();

                Console.WriteLine("calling authorize()");

                token = await authorizer.Authorize();
                Console.WriteLine("doDatasource(): Token=" + token.ToString());
            }

            DatasourceService datasourceService = new DatasourceService(token);

            if (operations.HasFlag(Operations.C))
            {
                Datasource newDatasource = DatasourceHelper.GenerateObject();
                Datasource datasource = await datasourceService.CreateObject(newDatasource);
                Console.WriteLine("Datasource Create: " + datasource.ToString());
            }

            if (operations.HasFlag(Operations.R))
            {
                Datasource datasource = await datasourceService.ReadObject();
                Console.WriteLine("Datasource Read: " + datasource.ToString());
            }

            if (operations.HasFlag(Operations.U))
            {
                Datasource newDatasource = DatasourceHelper.GenerateObject();
                Datasource datasource = await datasourceService.UpdateObject(newDatasource);
                Console.WriteLine("Datasource Update: " + datasource.ToString());
            }

            if (operations.HasFlag(Operations.D))
            {
                Datasource datasource = await datasourceService.DeleteObject();
                Console.WriteLine("Datasource Deleted.");
            }
            return (true);
        }

        private static async Task<bool> DoTerm(Operations operations)
        {

            if (token == null)
            {
                Authorizer authorizer = new Authorizer();

                Console.WriteLine("calling authorize()");

                token = await authorizer.Authorize();

                Console.WriteLine("doTerm(): Token=" + token.ToString());
            }

            TermService termService = new TermService(token);

            if (operations.HasFlag(Operations.C))
            {
                Term newTerm = TermHelper.GenerateObject();
                Term term = await termService.CreateObject(newTerm);
                Console.WriteLine("Term Create: " + term.ToString());
            }

            if (operations.HasFlag(Operations.R))
            {
                Term term = await termService.ReadObject();
                Console.WriteLine("Term Read: " + term.ToString());
            }

            if (operations.HasFlag(Operations.U))
            {
                Term newTerm = TermHelper.GenerateObject();
                Term term = await termService.UpdateObject(newTerm);
                Console.WriteLine("Term Update: " + term.ToString());
            }

            if (operations.HasFlag(Operations.D))
            {
                Term term = await termService.DeleteObject();
                Console.WriteLine("Term Delete: " + term.ToString());
            }
            return (true);
        }

        private static async Task<bool> DoCourse(Operations operations)
        {

            if (token == null)
            {
                Authorizer authorizer = new Authorizer();

                Console.WriteLine("calling authorize()");

                token = await authorizer.Authorize();

                Console.WriteLine("doCourse(): Token=" + token.ToString());
            }

            CourseService courseService = new CourseService(token);

            if (operations.HasFlag(Operations.C))
            {
                Course newCourse = CourseHelper.GenerateObject();
                Course course = await courseService.CreateObject(newCourse);
                Console.WriteLine("Course Create: " + course.ToString());
            }

            if (operations.HasFlag(Operations.R))
            {
                Course course = await courseService.ReadObject();
                Console.WriteLine("Course Read: " + course.ToString());
            }

            if (operations.HasFlag(Operations.U))
            {
                Course newCourse = CourseHelper.GenerateObject();
                Course course = await courseService.UpdateObject(newCourse);
                Console.WriteLine("Course Update: " + course.ToString());
            }

            if (operations.HasFlag(Operations.D))
            {
                Course course = await courseService.DeleteObject();
                Console.WriteLine("Course Delete: " + course.ToString());
            }
            return (true);
        }

        private static async Task<bool> DoUser(Operations operations)
        {

            if (token == null)
            {
                Authorizer authorizer = new Authorizer();

                Console.WriteLine("calling authorize()");

                token = await authorizer.Authorize();

                Console.WriteLine("doUser(): Token=" + token.ToString());
            }

            UserService userService = new UserService(token);

            if (operations.HasFlag(Operations.C))
            {
                User newUser = UserHelper.GenerateObject();
                User user = await userService.CreateObject(newUser);
                Console.WriteLine("User Create: " + user.ToString());
            }

            if (operations.HasFlag(Operations.R))
            {
                User user = await userService.ReadObject();
                Console.WriteLine("User Read: " + user.ToString());
            }

            if (operations.HasFlag(Operations.U))
            {
                User newUser = UserHelper.GenerateObject();
                User user = await userService.UpdateObject(newUser);
                Console.WriteLine("User Update: " + user.ToString());
            }

            if (operations.HasFlag(Operations.D))
            {
                User user = await userService.DeleteObject();
                Console.WriteLine("User Delete: " + user.ToString());
            }
            return (true);
        }

        private static async Task<bool> DoMembership(Operations operations)
        {

            if (token == null)
            {
                Authorizer authorizer = new Authorizer();

                Console.WriteLine("calling authorize()");

                token = await authorizer.Authorize();

                Console.WriteLine("doMembership(): Token=" + token.ToString());
            }

            MembershipService membershipService = new MembershipService(token);

            if (operations.HasFlag(Operations.C))
            {
                Membership newMembership = MembershipHelper.GenerateObject();
                Membership membership = await membershipService.CreateObject(newMembership);
                Console.WriteLine("Membership Create: " + membership.ToString());
            }

            if (operations.HasFlag(Operations.R))
            {
                Membership membership = await membershipService.ReadObject();
                Console.WriteLine("Membership Read: " + membership.ToString());
            }

            if (operations.HasFlag(Operations.U))
            {
                Membership newMembership = MembershipHelper.GenerateObject();
                Membership membership = await membershipService.UpdateObject(newMembership);
                Console.WriteLine("Membership Update: " + membership.ToString());
            }

            if (operations.HasFlag(Operations.D))
            {
                Membership membership = await membershipService.DeleteObject();
                Console.WriteLine("Membership Delete: " + membership.ToString());
            }
            return (true);
        }

        private static async Task<bool> DoAll()
        {
            Operations cruOperations = new Operations();
            Operations dOperations = new Operations();

            cruOperations = Operations.C | Operations.R | Operations.U;
            dOperations = Operations.D;

            bool result = await DoDatasource(cruOperations);

            result = await DoTerm(cruOperations);

            result = await DoCourse(cruOperations);

            result = await DoUser(cruOperations);

            result = await DoMembership(cruOperations);

            result = await DoMembership(dOperations);

            result = await DoUser(dOperations);

            result = await DoCourse(dOperations);

            result = await DoTerm(dOperations);

            result = await DoDatasource(dOperations);

            return (true);
        }
    }
}

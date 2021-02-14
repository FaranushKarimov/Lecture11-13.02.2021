using System;
using System.Collections.Generic;
using System.Threading;

namespace Lecture11_13._02._2021
{
    class Program
    {
        class Balance
        {
            public int id { get; set; }
            public decimal balanceBefore { get; set; }
            public decimal balanceAfter { get; set; }
            public Balance(int id, decimal balanceBefore, decimal balanceAfter)
            {
                this.id = id;
                this.balanceAfter = balanceAfter;
                this.balanceBefore = balanceBefore;
            }
        }
        public static void BalanceChecking(object obj)
        {
            Console.Clear();
            Balance balance = (Balance)obj;
            int id = balance.id;
            decimal before = balance.balanceBefore;
            decimal after = balance.balanceAfter;
            if (before - after != 0)
            {
                string sign = "";
                if (after - before > 0)
                {
                    sign = "+";
                }
                else
                {
                    sign = "-";
                }
                System.Console.WriteLine($"ID = {id}\nBalance was {before} now {after}\nDifference: {sign} {Math.Abs(before - after)}");
                System.Console.WriteLine("Press any key to continue");
            }
        }

        static object locker = new Object();
        public static List<Client> clients = new List<Client>();
        static void Main(string[] args)
        {

            bool working = true;

            System.Console.WriteLine("");
            while (working)
            {

                Console.Clear();
                System.Console.Write("Options: \n1.Show All Clients Info\n2.Show By Id\n3.Delete Client by ID\n4.Add new Client\n5.Update Client Info\n6.Exit\nYour choice: ");
                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    if (choice > 0 && choice <= 6)
                    {
                        switch (choice)
                        {
                            // Select 
                            case 1:
                                {
                                    Console.Clear();
                                    System.Console.WriteLine("Clients:");
                                    Thread selectAll = new Thread(Select);
                                    selectAll.Start();
                                    selectAll.Join();
                                }
                                break;
                            // Select By Id
                            case 2:
                                {

                                    Console.Clear();
                                    while (true)
                                    {
                                        int id;
                                        System.Console.Write("Enter id: ");
                                        if (int.TryParse(Console.ReadLine(), out id))
                                        {
                                            Thread selectById = new Thread(new ParameterizedThreadStart(SelectByID));
                                            selectById.Start(id);
                                            selectById.Join();
                                            break;
                                        }
                                    }
                                }
                                break;
                            // Delete By Id
                            case 3:
                                {

                                    Console.Clear();
                                    while (true)
                                    {
                                        int id;
                                        if (int.TryParse(Console.ReadLine(), out id))
                                        {
                                            Thread deleteById = new Thread(new ParameterizedThreadStart(DeleteByID));
                                            deleteById.Start(id);
                                            deleteById.Join();
                                            break;
                                        }
                                        else
                                        {
                                            ConsoleShow.Red("Error: Only number type");
                                        }
                                    }
                                }
                                break;
                            // Insert new Client
                            case 4:
                                {

                                    Console.Clear();
                                    System.Console.WriteLine("Add new Client: ");
                                    System.Console.Write("Firstname: ");
                                    string tempFirstname = Console.ReadLine();
                                    System.Console.Write("Secondname: ");
                                    string tempSecondname = Console.ReadLine();
                                    decimal tempBalance;
                                    while (true)
                                    {
                                        System.Console.Write("Balance: ");
                                        if (decimal.TryParse(Console.ReadLine(), out tempBalance))
                                        {
                                            break;
                                        }
                                    }

                                    Client tempClient = new Client(clients.Count + 1, tempFirstname, tempSecondname, tempBalance);
                                    Thread insertNewClient = new Thread(new ParameterizedThreadStart(Insert));
                                    insertNewClient.Start(tempClient);
                                    insertNewClient.Join();
                                }
                                break;
                            case 5:
                                {
                                    Console.Clear();
                                    int id;
                                    while (true)
                                    {
                                        System.Console.Write("Enter client id: ");
                                        if (int.TryParse(Console.ReadLine(), out id))
                                        {
                                            Thread updateById = new Thread(new ParameterizedThreadStart(Update));
                                            updateById.Start(id);
                                            updateById.Join();
                                            break;
                                        }
                                        else
                                        {
                                            ConsoleShow.Red("Error: Only number type");
                                        }
                                    }
                                }
                                break;
                            case 6:
                                {
                                    working = false;
                                }
                                break;
                        }
                    }
                }
                Thread.Sleep(1000);
            }


        }

        public static void Select()
        {
            lock (locker)
            {
                Console.Clear();
                if (clients.Count == 0)
                {
                    ConsoleShow.Red("No clients info");
                }
                else
                {
                    ConsoleShow.Green("Id\t\tFirstname\t\tSecondname\t\tBalance");
                    foreach (var client in clients)
                    {
                        System.Console.Write(client.Id);
                        for (int i = 0; i < 9 - client.Firstname.Length; i++)
                        {
                            System.Console.Write(" ");
                        }
                        System.Console.Write("\t\t" + client.Firstname);
                        for (int i = 0; i < 10 - client.Secondname.Length; i++)
                        {
                            System.Console.Write(" ");
                        }
                        System.Console.Write("\t\t" + client.Secondname);
                        String t = client.Balance.ToString();
                        for (int i = 0; i < Math.Abs(10 - t.Length); i++)
                        {
                            System.Console.Write(" ");
                        }
                        System.Console.WriteLine("\t\t" + client.Balance);
                    }
                }
            }
        }
        public static void SelectByID(object obj)
        {
            lock (locker)
            {
                int id = (int)obj;
                if (checkId(id) == true)
                {
                    Console.Clear();
                    ConsoleShow.Green("Id\t\tFirstname\t\tSecondname\t\tBalance");
                    Client tempClient = clients[id - 1];
                    System.Console.Write(tempClient.Id);
                    for (int i = 0; i < 9 - tempClient.Firstname.Length; i++)
                    {
                        System.Console.Write(" ");
                    }
                    System.Console.Write("\t\t" + tempClient.Firstname);
                    for (int i = 0; i < 10 - tempClient.Secondname.Length; i++)
                    {
                        System.Console.Write(" ");
                    }
                    System.Console.Write("\t\t" + tempClient.Secondname);
                    String t = tempClient.Balance.ToString();
                    for (int i = 0; i < Math.Abs(10 - t.Length); i++)
                    {
                        System.Console.Write(" ");
                    }
                    System.Console.WriteLine("\t\t" + tempClient.Balance);
                }
                else
                {
                    Console.Clear();
                    ConsoleShow.Red("Error: No such ID found");
                }
            }
        }
        public static void DeleteByID(object obj)
        {
            int id = (int)obj;
            if (checkId(id) == true)
            {
                //int index = 0; 
                foreach (var client in clients)
                {
                    if (client.Id == id)
                    {
                        clients.Remove(client);
                        break;
                    }
                    //index++; 
                }
                Console.Clear();
                ConsoleShow.Green<string>($"Client #{id} has been deleted");

            }
        }
        public static void Insert(object obj)
        {
            Console.Clear();
            Client client = (Client)obj;
            clients.Add(client);
            ConsoleShow.Green<string>($"{client.Firstname} has been added");
        }
        //! Develop
        public static void Update(object obj)
        {
            int id = (int)obj;
            if (checkId(id) == false)
            {
                Console.Clear();
                ConsoleShow.Red<string>("No such id found");
            }
            else
            {
                Client tempClient = clients[id - 1];
                bool working = true;
                while (working)
                {
                    Console.Clear();
                    System.Console.Write("What you want to change?: \n1.Firstname\n2.Secondname\n3.Balance\n4.Exit\nYour choice: ");
                    int choice;
                    if (int.TryParse(Console.ReadLine(), out choice))
                    {
                        if (choice > 0 && choice <= 4)
                        {
                            switch (choice)
                            {
                                case 1:
                                    {
                                        System.Console.Write("Firstname: ");
                                        clients[id - 1].Firstname = Console.ReadLine();
                                        ConsoleShow.Green<String>("Updated");
                                        Thread.Sleep(500);
                                    }
                                    break;
                                case 2:
                                    {
                                        System.Console.Write("Secondname: ");
                                        clients[id - 1].Secondname = Console.ReadLine();
                                        ConsoleShow.Green<String>("Updated");
                                        Thread.Sleep(500);
                                    }
                                    break;
                                case 3:
                                    {
                                        System.Console.Write("Balance: ");
                                        decimal tempBalance;
                                        if (decimal.TryParse(Console.ReadLine(), out tempBalance))
                                        {
                                            decimal last = clients[id - 1].Balance;
                                            clients[id - 1].Balance = tempBalance;
                                            ConsoleShow.Green<String>("Updated");
                                            Balance balance = new Balance(id, last, tempBalance);
                                            TimerCallback changeInClientBalance = new TimerCallback(BalanceChecking);
                                            Timer checkBalance = new Timer(changeInClientBalance, balance, 0, -1);
                                        }
                                    }
                                    break;
                                case 4:
                                    {
                                        working = false;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
        public static bool checkId(int id)
        {
            foreach (var client in Program.clients)
            {
                if (client.Id == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

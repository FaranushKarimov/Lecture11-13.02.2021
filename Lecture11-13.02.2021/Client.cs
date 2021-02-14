using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture11_13._02._2021
{
    public class Client
    {
        public int Id { get; set; }
        public String Firstname { get; set; }
        public String Secondname { get; set; }
        public decimal Balance { get; set; }
        public Client(int Id, String Firstname, String Secondname, decimal Balance)
        {
            this.Id = Id;
            this.Firstname = Firstname;
            this.Secondname = Secondname;
            this.Balance = Balance;
        }
    }
}

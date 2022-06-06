using CateringSedapAPI.Entitties;
using static CateringSedapAPI.Entitties.User;

namespace CateringSedapAPI.Factories
{
    public class UserFactory
    {

        public User CreateCustomer(string username, string password, string name, string address)
        {
            return new Customer(username, password, name, UserRole.customer, address);
        }

        public User CreateAdmin(string username, string password, string name, string jobPosition)
        {
            return new Admin(username, password, name, UserRole.admin, jobPosition);
        }

        public User CreateDriver(string username, string password, string name, string bikeNumber)
        {
            return new Driver(username, password, name, UserRole.driver, bikeNumber);
        }
    }
}

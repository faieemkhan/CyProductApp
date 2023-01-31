using System;

namespace MVCCoreApplication.UserException
{
    public class UserAlreadyExixtsException:ApplicationException
    {
        public  UserAlreadyExixtsException()
        {

        }
        public UserAlreadyExixtsException(string message):base(message) 
        { 
        
        }

    }
}

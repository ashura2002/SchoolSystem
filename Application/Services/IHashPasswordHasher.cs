using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IHashPasswordHasher
    {
        string Hash(string password);
        bool Verify(string password, string hashedPassword);
    }
}

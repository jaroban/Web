using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public struct RegistrationResult
{
    public bool Success;
    public List<string> AlreadyRegisteredEmails;
}
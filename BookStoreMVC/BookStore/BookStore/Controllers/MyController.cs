using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.Controllers
{
  public class MyController : IController
  {
    //При обращении к любому контроллеру система передает в него контекст запроса. 
    //В этот контекст запроса включается все: куки, отправленные данные форм, строки запроса, идентификационные данные пользователя и т.д. 
    //Реализация интерфейса IController позволяет получить этот контекст запроса в методе Execute через параметр RequestContext. 

    public void Execute(RequestContext requestContext)
    {
      string ip = requestContext.HttpContext.Request.UserHostAddress;
      var responce = requestContext.HttpContext.Response;
      responce.Write("<h2>Your IP adress:" + ip + "</h2>");
    }
  }
        
}
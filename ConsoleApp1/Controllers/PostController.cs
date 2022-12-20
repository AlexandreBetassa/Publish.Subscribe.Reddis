using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using System.Web.Http;

namespace ConsoleApp1.Controllers.v1
{
    public class PostController : ApiController
    {
        [HttpGet]
        [Route("api/posts")]
        public HttpResponseMessage Get()
        {
            IList<string> posts = new List<string>()
            {
                "Conteudo do post 1",
                "Conteudo do post 2"
            };

            return Request.CreateResponse(HttpStatusCode.OK, posts);
        }

        [HttpGet]
        [Route("api/posts/{1}")]
        public HttpResponseMessage Get(int id)
        {
            string post = "Conteudo do post";
            return Request.CreateResponse(HttpStatusCode.OK, post);
        }
    }
}

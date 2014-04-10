using System.Web.Mvc;
using Moq;

namespace UnitTest.Utils.Test
{
    public class ModelStateTestController : Controller
    {
        public ModelStateTestController()
        {
            ControllerContext = (new Mock<ControllerContext>()).Object;
        }

        public bool TestTryValidateModel(object model)
        {
            return TryValidateModel(model);
        }
    }
}

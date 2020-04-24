namespace Lands.ViewModels
{
    using Models;
    //esta clase se hace porque no se puede poner el command del tap en landsviewmodel(pierde el contexto) ni en la clase Lands(para respetar la arquitectura)
    public class LandItemViewModel : Lands
    {

    }
}

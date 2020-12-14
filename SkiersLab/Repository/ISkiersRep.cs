using System.Threading.Tasks;

namespace SkiersLab.Repository
{
    public interface ISkiersRep
    {
        public  void ParallelWork();


        public  void ChangeAge(int skierId);


        public  void ChangeRaceCountry(int skierId);
       
    }
}
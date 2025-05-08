using _00.Work.MOON._01.Script.Entities;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Players
{
    public interface IHitable
    {
        public void Hit(Entity hiter);
    }
}

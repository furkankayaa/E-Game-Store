using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public static class Helper
    {
        public static bool isPurchased(GameDetailResponse game, List<List<GameOrderLink>> p)
        {
            foreach (var i in p)
            {
                foreach (var j in i)
                {
                    if (j.Game.GameName == game.GameName)
                        return true;
                }
            }
            return false;
        }

        public static bool isCartItem(GameDetailResponse game, List<CartItemDetail> cart)
        {
            if (cart.Where(x => x.GameName == game.GameName).FirstOrDefault() != null)
            {
                return true;
            }
            return false;
        }
    }
}

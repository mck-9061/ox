using System;
using System.Collections.Generic;
using System.Text;

namespace NoughtsAndCrosses {
    class GridGraphic {
        public GridGraphic() {

        }

        public void DrawGridFromArray(string[] array) {
            if (array.Length != 9) return;

            //|-----------|
            //| x | x | x |
            //|-----------|
            //| x | x | x |
            //|-----------|
            //| x | x | x |
            //|-----------|

            Console.WriteLine($"    0   1   2  ");
            Console.WriteLine($"  |-|---|---|-|");
            Console.WriteLine($"  | {array[0]} | {array[1]} | {array[2]} |");
            Console.WriteLine($"  |-----------|");
            Console.WriteLine($"3>| {array[3]} | {array[4]} | {array[5]} |<5");
            Console.WriteLine($"  |-----------|");
            Console.WriteLine($"  | {array[6]} | {array[7]} | {array[8]} |");
            Console.WriteLine($"  |-|---|---|-|");
            Console.WriteLine($"    6   7   8 ");

        }
    }
}

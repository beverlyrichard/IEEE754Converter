using System;

namespace IEEE754Converter {
    class Program {
        static void Main(string[] args){
            
        //1 | 10000101 | 11101000100010000101010
        Console.WriteLine(Converter.IEEE754(-122.13313361));
        
        //0 | 10000101 | 00110110010000111001010
        Console.WriteLine(Converter.IEEE754(77.566));
        
        //0 | 01111110 | 10001101001000110100111
        Console.WriteLine(Converter.IEEE754(0.77566));
        
        //1 | 01110111 | 10111001000011101010100
        Console.WriteLine(Converter.IEEE754(-0.00673));
        }
    }
}
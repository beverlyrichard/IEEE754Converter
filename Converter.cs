using System;
using System.Text;

namespace IEEE754Converter{
    public class Converter{
        public static string IEEE754(double number){
            // first store if the number is positive or negative
            int signBit;
            if(number > 0){
                signBit = 0;
            }
            else {              // number is negative
                signBit = 1;
            }

            // get absolute value
            number = Math.Abs(number);

            // see if number is a decimal between 0 and 1
            if(number < 1 && number > 0){
                double result;
                int i = -1;

                // divide number by 2^i until it is greater than one
                // decrease i each iteration
                result = number / (Math.Pow(2, i));
                while(result < 1){
                    i--;
                    result = number / (Math.Pow(2, i));
                }

                // since the exponent is negative, subtract from 127
                int exponent = 127 + i;
                int[] exponentBinary = convertToBinary(exponent);

                double deci = Math.Abs(((int) result) - result);
                int[] mantissa = convertDecimalToBinary(deci);

                // now convert arrays to strings with StringBuilder
                StringBuilder expsb = new StringBuilder();
                for(int j = 0; j < exponentBinary.Length; j++){
                    expsb.Append(exponentBinary[j]);
                }
                StringBuilder mansb = new StringBuilder();
                for(int j = 0; j < mantissa.Length; j++){
                    mansb.Append(mantissa[j]);
                }

                // maybe create a method that builds the output

                string output = signBit + " | " + expsb.ToString() + " | " + mansb.ToString();
                return output;
            }
            else {      // number is greater than one

                // get the whole number to the left of the decimal
                int wholeNum = (int) number;

                // store the decimal part of the number
                double deci = number - wholeNum;

                int[] wholeNumBinary = convertToBinary(wholeNum);

                int shifted = shift(wholeNumBinary);

                // now that we have exponent add to 127
                int exponent = 127 + shifted;

                int[] exponentBinary = convertToBinary(exponent);

                // now add shifted wholeNumBinary and decimalBinary to create the mantissa
                
                int[] mantissa = convertDecimalToBinary(deci);
                // first fill the end of the mantissa from the decimal binary
                for(int i = mantissa.Length - 1; i >= shifted; i--){
                    mantissa[i] = mantissa[i - shifted];
                }
                // now fill the beginning of the mantissa with the shifted bits from wholeNumBinary
                int offset = wholeNumBinary.Length - shifted;   // this indicates which bits from wholeNumBinary to take
                for(int i = 0; i < shifted; i++){
                    mantissa[i] = wholeNumBinary[offset];
                    offset++;
                }
                StringBuilder expsb = new StringBuilder();
                for(int i = 0; i < exponentBinary.Length; i++){
                    expsb.Append(exponentBinary[i]);
                }
                StringBuilder mansb = new StringBuilder();
                for(int i = 0; i < mantissa.Length; i++){
                    mansb.Append(mantissa[i]);
                }

                String output = signBit + " | " + expsb.ToString() + " | " + mansb.ToString();
                return output;
            }
        }
        
        // this method converts a whole number to its binary equivalent
        public static int[] convertToBinary(int number){
            int[] reversedBinary = new int[9];
            int i = 1; 
            int result, remainder;
            result = number / 2;
            remainder = number % 2;
            reversedBinary[0] = remainder;

            // keep dividing by 2 until we get a result of 0
            while(result != 0){
                number = result;
                result = number / 2;
                remainder = number % 2;
                reversedBinary[i] = remainder;
                i++;
            }
            
            number = result;
            remainder = number % 2;
            reversedBinary[i] = remainder;

            // now reverse the array
            int[] binary = new int[8];
            int j = 8;
            for(int k = 0; k < 8; k++){
                binary[j-1] = reversedBinary[k];
                j--;
            }
            return binary;
        }

        // This method converts a decimal number to its binary equivalent
        public static int[] convertDecimalToBinary(double number){

            int[] binary = new int[23];

            double result = number;
            int remainder;
            int i = 0;
            while( i < 23){
                result = result * 2;
                remainder = (int) result;
                result = Math.Abs(remainder - result);
                binary[i] = remainder;
                i++;
            }
            return binary;
        }

        public static int shift(int[] binary){
            int exponent = 0;

            for(int j = 0; j < binary.Length; j++){
                if(binary[j] == 1){
                    exponent = binary.Length - (j + 1);
                    break;
                }
            }

            return exponent;
        }
    }
}
export class Calculator {
    public Add(a: number, b: number) {
        return a + b;
    }

    public Subtract(a: number, b: number): number {
        return a - b;
    }

    public Multiply(a: number, b: number): number {
        return a * b;
    }

    public Divide(a: number, b: number): number {
        if (b === 0) {
            throw new Error("Division by zero is not allowed.");
        }
        return a / b;
    }

    public Sqrt(a: number): number {
        if (a < 0) {
            throw new Error("Square root of a negative number is not allowed.");
        }
        return Math.sqrt(a);
    }

    public Power(a: number, b: number): number {
        return Math.pow(a, b);
    }
}
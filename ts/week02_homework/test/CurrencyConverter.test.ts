import { CurrencyConverter } from "../src/CurrencyConverter"
import { IExchangeRateService } from "../src/ExchangeRateService"
import  {mock, mockReset} from "jest-mock-extended"

const mockedExchangeRateService=mock<IExchangeRateService>();
describe('',()=>{

    let currencyConverter:CurrencyConverter;
    beforeEach(()=>{
        mockReset(mockedExchangeRateService);
        currencyConverter= new CurrencyConverter(mockedExchangeRateService)
    });

    it('',()=>{

        //Arrange
        mockedExchangeRateService.getExchangeRate.calledWith('huf','eur').mockReturnValue(22);
        const expected=111*22;
        //ACT
        let actual=currencyConverter.Convert(111,'huf','eur');

        //Assert
        expect(actual).toBe(expected);
        expect(mockedExchangeRateService.getExchangeRate).toHaveBeenCalledTimes(1);
        expect(mockedExchangeRateService.getExchangeRate).toHaveReturnedWith(22);

    });

    describe('Convert', () => {
        it('should convert amounts correctly using the exchange rate', () => {
            mockedExchangeRateService.getExchangeRate.mockReturnValue(2); // Assuming 1 USD = 2 EUR for simplicity
          expect(currencyConverter.Convert(100, 'USD', 'EUR')).toEqual(200);
        });
    
        it('throws an error for invalid amount input', () => {
          expect(() => currencyConverter.Convert(NaN, 'USD', 'EUR')).toThrow('Invalid amount input.');
        });
      });
    
      describe('GenerateConversionReport', () => {
        it('should generate a conversion report for a given date range', () => {
          // Setup mock to return a fixed exchange rate
          mockedExchangeRateService.getExchangeRate.mockReturnValue(2);
          const startDate = new Date(2020, 0, 1);
          const endDate = new Date(2020, 0, 3);
    
          // Assuming a fixed amount of 100 for simplicity
          const expectedReport = `Conversion Report:\n200\n200\n200`;
          expect(currencyConverter.GenerateConversionReport('USD', 'EUR', startDate, endDate)).toEqual(expectedReport);
        });
    
        it('should throw an error when unable to fetch exchange rate', () => {
          // Setup mock to simulate an error
          mockedExchangeRateService.getExchangeRate.mockImplementation(() => {
            throw new Error('Unable to fetch exchange rate.');
          });
          const startDate = new Date(2020, 0, 1);
          const endDate = new Date(2020, 0, 3);
    
          expect(() => currencyConverter.GenerateConversionReport('USD', 'EUR', startDate, endDate)).toThrow('Unable to fetch exchange rate.');
        });
      });
});
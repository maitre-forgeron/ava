import { TestBed } from '@angular/core/testing';
import { CardClassGeneratorService } from './card-class-generator.service';

describe('CardClassGeneratorService', () => {
  let service: CardClassGeneratorService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CardClassGeneratorService],
    });
    service = TestBed.inject(CardClassGeneratorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should return a string containing "card" as the base class', () => {
    const result = service.computateCardClass();
    expect(result).toContain('card');
  });

  it('should return a valid card variation class (e.g., "card-1" to "card-9")', () => {
    const result = service.computateCardClass();
    const cardVariations = service['getCardClassVariation']();
    const isValidVariation = cardVariations.some((variation) => result.includes(variation));

    expect(isValidVariation).toBeTrue();
  });

  it('should return a random integer within the specified interval', () => {
    const min = 1;
    const max = 9;
    const randomInt = service['randomIntFromInterval'](min, max);

    expect(randomInt).toBeGreaterThanOrEqual(min);
    expect(randomInt).toBeLessThanOrEqual(max);
  });
});

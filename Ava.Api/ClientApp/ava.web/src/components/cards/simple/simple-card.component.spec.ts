import { ComponentFixture, TestBed } from '@angular/core/testing';
import { SimpleCardComponent } from './simple-card.component';
import { CardClassGeneratorService } from '../card-class-generator.service';

describe('SimpleCardComponent', () => {
  let component: SimpleCardComponent;
  let fixture: ComponentFixture<SimpleCardComponent>;
  let classGeneratorServiceSpy: jasmine.SpyObj<CardClassGeneratorService>;

  beforeEach(() => {
    // Create a spy for CardClassGeneratorService
    classGeneratorServiceSpy = jasmine.createSpyObj('CardClassGeneratorService', ['computateCardClass']);

    // Configure the testing module
    TestBed.configureTestingModule({
      imports: [SimpleCardComponent],
      providers: [
        { provide: CardClassGeneratorService, useValue: classGeneratorServiceSpy },
      ],
    }).compileComponents();

    // Create the component fixture
    fixture = TestBed.createComponent(SimpleCardComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should set the card class on initialization', () => {
    // Arrange: Set a mock return value for the service method
    const mockClass = 'card card-3';
    classGeneratorServiceSpy.computateCardClass.and.returnValue(mockClass);

    // Act: Trigger ngOnInit
    component.ngOnInit();

    // Assert: Check if the class property was set correctly
    expect(component.class).toBe(mockClass);
    expect(classGeneratorServiceSpy.computateCardClass).toHaveBeenCalled();
  });
});

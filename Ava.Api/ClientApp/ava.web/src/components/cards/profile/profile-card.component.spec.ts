import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProfileCardComponent } from './profile-card.component';
import { CardClassGeneratorService } from '../card-class-generator.service';

describe('ProfileCardComponent', () => {
  let component: ProfileCardComponent;
  let fixture: ComponentFixture<ProfileCardComponent>;
  let classGeneratorServiceSpy: jasmine.SpyObj<CardClassGeneratorService>;

  beforeEach(() => {
    // Create a spy object for CardClassGeneratorService
    classGeneratorServiceSpy = jasmine.createSpyObj('CardClassGeneratorService', ['computateCardClass']);

    // Configure the testing module
    TestBed.configureTestingModule({
      imports: [ProfileCardComponent],
      providers: [
        { provide: CardClassGeneratorService, useValue: classGeneratorServiceSpy },
      ],
    }).compileComponents();

    // Create the component fixture
    fixture = TestBed.createComponent(ProfileCardComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize input properties correctly', () => {
    // Arrange: Use Angular's signal() function to create mock signals
    fixture.componentRef.setInput("fullName", 'John Doe');
    fixture.componentRef.setInput("proffession", 'Psychiatrist');
    fixture.componentRef.setInput("imageLink", 'https://example.com/profile.jpg');
    fixture.componentRef.setInput("description", 'Experienced professional with a focus on mental health.');
    // Act: Trigger change detection
    fixture.detectChanges();

    // Assert: Verify input properties
    expect(component.fullName()).toBe('John Doe');
    expect(component.proffession()).toBe('Psychiatrist');
    expect(component.imageLink()).toBe('https://example.com/profile.jpg');
    expect(component.description()).toBe('Experienced professional with a focus on mental health.');
  });

  it('should set the card class on initialization', () => {
    // Arrange: Set a mock return value for the service method
    const mockClass = 'card card-4';
    classGeneratorServiceSpy.computateCardClass.and.returnValue(mockClass);

    // Act: Trigger ngOnInit
    component.ngOnInit();

    // Assert: Check if the class property was set correctly
    expect(component.class).toBe(mockClass);
    expect(classGeneratorServiceSpy.computateCardClass).toHaveBeenCalled();
  });
});

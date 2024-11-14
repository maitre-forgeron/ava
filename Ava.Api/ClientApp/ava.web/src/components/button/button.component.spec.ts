import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ButtonComponent } from './button.component';
import { signal } from '@angular/core';

describe('ButtonComponent', () => {
  let component: ButtonComponent;
  let fixture: ComponentFixture<ButtonComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ButtonComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(ButtonComponent);
    component = fixture.componentInstance;
  });

  it('should create the component', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize input properties correctly', () => {
    // Arrange: Set mock input signals using Angular's signal API
    fixture.componentRef.setInput("buttonText", 'Click Me');
    fixture.componentRef.setInput("size", 'small');

    // Act: Trigger change detection
    fixture.detectChanges();

    // Assert: Verify the signal values
    expect(component.buttonText()).toBe('Click Me');
    expect(component.size()).toBe('small');
  });

  it('should compute the correct button class for small size', () => {
    // Arrange: Set the size input signal to "small"
    fixture.componentRef.setInput("size", 'small');

    // Act: Call ngOnInit and trigger change detection
    component.ngOnInit();
    fixture.detectChanges();

    // Assert: Verify the computed class
    expect(component.class).toBe('btn-default btn-default-small');
  });

  it('should compute the correct button class for large size', () => {
    // Arrange: Set the size input signal to "large"
    fixture.componentRef.setInput("size", 'large');

    // Act: Call ngOnInit and trigger change detection
    component.ngOnInit();
    fixture.detectChanges();

    // Assert: Verify the computed class
    expect(component.class).toBe('btn-default btn-default-large');
  });

  it('should compute the default class when size is undefined', () => {
    // Arrange: Set the size input signal to undefined
    fixture.componentRef.setInput("size", undefined);

    // Act: Call ngOnInit and trigger change detection
    component.ngOnInit();
    fixture.detectChanges();

    // Assert: Verify the computed class is empty
    expect(component.class).toBe('');
  });
});

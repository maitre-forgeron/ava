import { CommonModule } from "@angular/common";
import { Component, Output, EventEmitter } from "@angular/core";
import { FormsModule } from "@angular/forms";

@Component({
    selector: "ava-therapists-filter",
    standalone: true,
    templateUrl: './therapists-filter.component.html',
    styleUrls: ['./therapists-filter.component.scss'],
    imports: [CommonModule, FormsModule]
})
export class TherapistsFilterComponent {
    @Output() filtersChanged = new EventEmitter<any>();

    rating: number | null = null;
    specialization: string | null = null;

    onFilterChange(): void {
        this.filtersChanged.emit({
            rating: this.rating
            // more here
        });
    }
}

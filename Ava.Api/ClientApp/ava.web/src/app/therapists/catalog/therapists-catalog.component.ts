import { Component } from "@angular/core";
import { TherapistsFilterComponent } from "./filter/therapists-filter.component";
import { ProfileCardComponent } from "../../../components/cards/profile/profile-card.component";
import { TherapistsListComponent } from "./list/therapists-list.component";
import { Therapist } from "../../models/therapist.model";
import { TherapistService } from "../../services/therapist.service";

@Component({
    standalone: true,
    templateUrl: './therapists-catalog.component.html',
    styleUrl: './therapists-catalog.component.scss',
    imports: [TherapistsFilterComponent, ProfileCardComponent, TherapistsListComponent]
})
export class TherapistsCatalogComponent {

    therapists: Therapist[] = [];
    filteredTherapists: Therapist[] = [];

    constructor(private therapistService: TherapistService) { }

    ngOnInit(): void {
        this.therapistService.getAllTherapists().subscribe(
            (data) => {
                this.therapists = data;
                this.filteredTherapists = data;
            },
            (error) => console.error('Error fetching therapists', error)
        );
    }

    applyFilters(filters: any): void {
        this.filteredTherapists = this.therapists.filter(therapist =>
            (!filters.rating || therapist.rating >= filters.rating)
            // more here
        );
    }
}

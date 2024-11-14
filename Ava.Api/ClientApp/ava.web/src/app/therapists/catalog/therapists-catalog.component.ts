import { Component } from "@angular/core";
import { TherapistsFilterComponent } from "./filter/therapists-filter.component";
import { ProfileCardComponent } from "../../../components/cards/profile/profile-card.component";
import { TherapistsListComponent } from "./list/therapists-list.component";

@Component({
    standalone: true,
    templateUrl: './therapists-catalog.component.html',
    styleUrl: './therapists-catalog.component.scss',
    imports: [TherapistsFilterComponent, ProfileCardComponent, TherapistsListComponent]
})
export class TherapistsCatalogComponent {

}
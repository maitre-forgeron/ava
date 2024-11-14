import { Component } from "@angular/core";
import { ProfileCardComponent } from "../../../../components/cards/profile/profile-card.component";

@Component({
    selector: 'ava-therapists-list',
    standalone: true,
    templateUrl: './therapists-list.component.html',
    styleUrl: './therapists-list.component.scss',
    imports: [ProfileCardComponent]
})
export class TherapistsListComponent {

}
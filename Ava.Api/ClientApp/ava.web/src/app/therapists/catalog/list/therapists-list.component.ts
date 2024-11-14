import { Component, Input } from "@angular/core";
import { Therapist } from "../../../models/therapist.model";
import { ProfileCardComponent } from "../../../../components/cards/profile/profile-card.component";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

@Component({
    selector: 'ava-therapists-list',
    standalone: true,
    templateUrl: './therapists-list.component.html',
    styleUrls: ['./therapists-list.component.scss'],
    imports: [ProfileCardComponent, CommonModule, FormsModule]
})
export class TherapistsListComponent {
    @Input() therapists: Therapist[] = [];
}

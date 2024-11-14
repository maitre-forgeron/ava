import { Component } from "@angular/core";
import { SimpleCardComponent } from "../cards/simple/simple-card.component";
import { ButtonComponent } from "../button/button.component";

@Component({
    selector: 'ava-landing',
    standalone: true,
    templateUrl: './landing.component.html',
    styleUrl: './landing.component.scss',
    imports: [SimpleCardComponent, ButtonComponent]
})
export class LandingComponent {

}
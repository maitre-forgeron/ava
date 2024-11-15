import { Component } from "@angular/core";
import { ButtonComponent } from "../button/button.component";

@Component({
    selector: 'ava-header',
    standalone: true,
    templateUrl: './header.component.html',
    styleUrl: './header.component.scss',
    imports: [ButtonComponent]
})
export class HeaderComponent {

}
import { Component, input, InputSignal, OnInit } from "@angular/core";

@Component({
    selector: 'ava-button',
    standalone: true,
    templateUrl: './button.component.html',
    styleUrl: './button.component.scss'
})
export class ButtonComponent implements OnInit {
    buttonText: InputSignal<string | undefined> = input<string>();
    size: InputSignal<"small" | "large" | undefined> = input<"small" | "large" | undefined>();

    public class: string = "";

    ngOnInit(): void {
        this.class = this.computateButtonClass();
    }

    private computateButtonClass(): string {
        const defaultButtonClass: string = "btn-default";
        let computatedButtonClass: string = defaultButtonClass;

        switch (this.size()) {
            case "small":
                computatedButtonClass = `${computatedButtonClass} btn-default-small`;
                break;
            case "large":
                computatedButtonClass = `${computatedButtonClass} btn-default-large`;
                break;
            default:
                computatedButtonClass = "";
                break;
        }

        return computatedButtonClass;
    }
}
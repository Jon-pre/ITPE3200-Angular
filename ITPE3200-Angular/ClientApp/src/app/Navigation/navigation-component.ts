import { Component } from '@angular/core';

@Component({
    selector: 'app-meny',
    templateUrl: './navigation-component.html'
})
export class NavMenuComponent {
    isExpanded = false;

    collapse() {
        this.isExpanded = false;
    }

    toggle() {
        this.isExpanded = !this.isExpanded;
    }
}
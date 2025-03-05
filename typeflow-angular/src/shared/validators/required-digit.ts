import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function requiredDigit(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        if(!control ) return null;

        const value = control.value;
        if (!value) {
          return null;
        }

        const hasDigit = /\p{N}/u.test(value);
        return hasDigit ? null : { requiredDigit: true };
    };
}
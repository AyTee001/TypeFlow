import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function requiredUppercase(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        if(!control ) return null;

        const value = control.value;
        if (!value) {
          return null;
        }

        const hasUppercase = /\p{Lu}/u.test(value);
        return hasUppercase ? null : { requiredUppercase: true };
    };
}
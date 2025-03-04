import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function requiredNonAlphanumeric(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        if(!control ) return null;

        const value = control.value;
        if (!value) {
          return null;
        }

        const hasNonAlphanumeric = /[^\p{L}\p{N}]/u.test(value);
        return hasNonAlphanumeric ? null : { requiredNonAlphanumeric: true };
    };
}
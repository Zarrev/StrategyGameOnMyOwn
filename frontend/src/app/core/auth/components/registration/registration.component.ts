import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../services/authentication.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  registerForm: FormGroup;
  submitted = false;
  returnUrl: string;
  checkPasswords = (group: FormGroup) => {
    const pass = group.get('password').value;
    const confirmPass = group.get('confirm_password').value;

    return pass === confirmPass ? null : { notSame: true };
  }

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService
  ) {
    // redirect to home if already logged in
    if (this.authenticationService.currentUserValue) {
      this.router.navigate(['/country']);
    }
  }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
      confirm_password: ['', Validators.required],
      city_name: ['', Validators.required]
    }, { validator: this.checkPasswords });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
  }

  // convenience getter for easy access to form fields
  get f() { return this.registerForm.controls; }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }

    this.authenticationService.registration(
      { Password: this.f.password.value, RepeatedPassword: this.f.confirm_password.value,
        Username: this.f.username.value, CountryName: this.f.city_name.value })
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate([this.returnUrl]);
        });
  }
}

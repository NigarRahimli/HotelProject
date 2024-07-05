import React, { useState } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faCheck,
  faTimes,
  faEye,
  faEyeSlash,
} from "@fortawesome/free-solid-svg-icons";

function SignUp() {
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [showRequirements, setShowRequirements] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [passwordRequirements, setPasswordRequirements] = useState({
    length: false,
    uppercase: false,
    lowercase: false,
    number: false,
    specialChar: false,
  });
  const [passwordError, setPasswordError] = useState("");
  const [confirmPasswordError, setConfirmPasswordError] = useState("");

  const handlePasswordChange = (e) => {
    const newPassword = e.target.value;
    setPassword(newPassword);
    setPasswordRequirements({
      length: newPassword.length >= 8,
      uppercase: /[A-Z]/.test(newPassword),
      lowercase: /[a-z]/.test(newPassword),
      number: /\d/.test(newPassword),
      specialChar: /[!@#$%^&*(),.?":{}|<>]/.test(newPassword),
    });
    setPasswordError("");
  };

  const handleConfirmPasswordChange = (e) => {
    const newConfirmPassword = e.target.value;
    setConfirmPassword(newConfirmPassword);
    if (!password) {
      setConfirmPasswordError("Please fill in the password first.");
    } else if (newConfirmPassword !== password) {
      setConfirmPasswordError("Passwords do not match.");
    } else {
      setConfirmPasswordError("");
    }
  };

  const toggleShowPassword = () => {
    setShowPassword(!showPassword);
  };

  const renderRequirement = (isMet, text) => (
    <div className="flex items-center">
      <FontAwesomeIcon
        icon={isMet ? faCheck : faTimes}
        className={`mr-2 ${isMet ? "text-green-500" : "text-red-500"}`}
      />
      <span className={isMet ? "text-green-500" : "text-red-500"}>{text}</span>
    </div>
  );

  const handleSubmit = (e) => {
    e.preventDefault();
    let valid = true;

    if (
      !passwordRequirements.length ||
      !passwordRequirements.uppercase ||
      !passwordRequirements.lowercase ||
      !passwordRequirements.number ||
      !passwordRequirements.specialChar
    ) {
      setPasswordError("Password does not meet the requirements.");
      valid = false;
    }

    if (!password) {
      setPasswordError("Please fill in the password.");
      valid = false;
    } else if (confirmPassword !== password) {
      setConfirmPasswordError("Passwords do not match.");
      valid = false;
    }

    if (valid) {
      // Submit form or perform any further actions
    }
  };

  return (
    <div className="flex min-h-full flex-1 flex-col justify-center px-6 py-12 lg:px-8">
      <div className="sm:mx-auto sm:w-full sm:max-w-sm">
        <img
          alt="Your Company"
          src="/images/logo.png"
          className="mx-auto h-10 w-auto"
        />
        <h2 className="mt-10 text-center text-2xl font-bold leading-9 tracking-tight text-gray-900">
          Sign up for an account
        </h2>
      </div>

      <div className="mt-10  sm:mx-auto sm:w-full sm:max-w-sm">
        <form onSubmit={handleSubmit} >
          <div>
            <label
              htmlFor="email"
              className="block text-sm font-medium leading-6 text-gray-900"
            >
              Email address
            </label>
            <div className="mt-2 pb-6">
              <input
                id="email"
                name="email"
                type="email"
                required
                autoComplete="email"
                className="block w-full rounded-md border border-gray-300 p-3 text-gray-900 shadow-sm placeholder:text-gray-400 focus:outline-none sm:text-sm sm:leading-6"
              />
            </div>
          </div>

          <div>
            <label
              htmlFor="password"
              className="block text-sm font-medium leading-6 text-gray-900"
            >
              Password
            </label>
            <div className="mt-2 relative">
              <input
                id="password"
                name="password"
                type={showPassword ? "text" : "password"}
                value={password}
                onChange={handlePasswordChange}
                onFocus={() => setShowRequirements(true)}
                onBlur={() => setShowRequirements(false)}
                required
                autoComplete="new-password"
                className="block w-full rounded-md border py-3 pl-3 pr-[45px] border-gray-300 text-gray-900 shadow-sm placeholder:text-gray-400 focus:outline-none sm:text-sm sm:leading-6"
              />
              <FontAwesomeIcon
                icon={showPassword ? faEyeSlash : faEye}
                className="absolute right-3 top-[25px] transform -translate-y-1/2 p-1 cursor-pointer text-gray-500"
                onClick={toggleShowPassword}
              />
              {showRequirements && (
                <div className="absolute top-[45px] left-0 mt-2 w-full rounded-md border border-gray-300 bg-white p-4 shadow-lg">
                  {renderRequirement(
                    passwordRequirements.length,
                    "At least 8 characters"
                  )}
                  {renderRequirement(
                    passwordRequirements.uppercase,
                    "At least one uppercase letter"
                  )}
                  {renderRequirement(
                    passwordRequirements.lowercase,
                    "At least one lowercase letter"
                  )}
                  {renderRequirement(
                    passwordRequirements.number,
                    "At least one number"
                  )}
                  {renderRequirement(
                    passwordRequirements.specialChar,
                    "At least one special character"
                  )}
                </div>
              )}
              <div className="mt-2 h-5">
                {passwordError && (
                  <div className="text-red-500 text-sm">{passwordError}</div>
                )}
              </div>
            </div>
          </div>

          <div>
            <label
              htmlFor="confirm-password"
              className="block text-sm font-medium leading-6 text-gray-900"
            >
              Confirm Password
            </label>
            <div className="mt-2 pb-3">
              <input
                id="confirm-password"
                name="confirm-password"
                type="password"
                value={confirmPassword}
                onChange={handleConfirmPasswordChange}
                required
                autoComplete="new-password"
                className="block w-full rounded-md border p-3 border-gray-300 text-gray-900 shadow-sm placeholder:text-gray-400 focus:border-[#101010] focus:outline-none sm:text-sm sm:leading-6"
              />
              <div className="mt-2 h-5 text-red-500 text-sm">
                {confirmPasswordError}
              </div>
            </div>
          </div>

          <div>
            <button
              type="submit"
              className="flex w-full justify-center rounded-md bg-[#101010] px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-[#484848] focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-[#101010]"
            >
              Sign up
            </button>
          </div>
        </form>

        <p className="mt-10 text-center text-sm text-gray-500">
          Already have an account?{" "}
          <a
            href="./signin"
            className="font-semibold leading-6 text-[#101010] hover:text-[#484848]"
          >
            Sign in
          </a>
        </p>
      </div>
    </div>
  );
}

export default SignUp;

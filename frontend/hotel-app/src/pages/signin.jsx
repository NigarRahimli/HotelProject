import React, { useState } from 'react';
import { useRouter } from 'next/router';
import { useAuth } from '../components/AuthProvider'; 
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faEye, faEyeSlash } from "@fortawesome/free-solid-svg-icons";
import { baseUrl } from '@/components/constant';

function SignIn() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [resendEmailStatus, setResendEmailStatus] = useState('');
  const { login } = useAuth();
  const router = useRouter();
  const { redirect } = router.query;

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError(''); 
    try {
      await login(email, password);
      if (redirect) {
        router.push(redirect); 
      } else {
        router.push('/'); 
      }
    } catch (error) {
     
        setError(error.message); 
      
    }
  };

  const resendConfirmationEmail = async () => {
    try {
      const response = await fetch(`${baseUrl}/api/account/resend-confirmation`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ email }),
      });

      if (response.ok) {
        setResendEmailStatus("Confirmation email sent successfully!");
      } else {
        const data = await response.json();
        setResendEmailStatus(data.message || "Failed to send confirmation email.");
      }
    } catch (error) {
      setResendEmailStatus("An error occurred while sending the confirmation email.");
    }
  };

  const toggleShowPassword = () => {
    setShowPassword(!showPassword);
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
          Sign in to your account
        </h2>
      </div>
      <div className="mt-10 sm:mx-auto sm:w-full sm:max-w-sm">
        <form onSubmit={handleSubmit} className="space-y-4">
          <label htmlFor="email" className="block text-sm font-medium leading-6 text-gray-900">
            Email address
          </label>
          <input
            id="email"
            name="email"
            type="email"
            required
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            autoComplete="email"
            className="block w-full rounded-md border p-3 border-gray-300 py-1.5 text-gray-900 shadow-sm placeholder:text-gray-400 focus:outline-none sm:text-sm sm:leading-6"
          />

          <label htmlFor="password" className="block text-sm font-medium leading-6 text-gray-900">
            Password
          </label>
          <div className="mt-2 relative">
            <input
              id="password"
              name="password"
              type={showPassword ? "text" : "password"}
              required
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              autoComplete="current-password"
              className="block w-full rounded-md border p-3 border-gray-300 py-1.5 text-gray-900 shadow-sm placeholder:text-gray-400 focus:outline-none sm:text-sm sm:leading-6"
            />
            <div className="absolute inset-y-0 right-0 flex items-center pr-3">
              <FontAwesomeIcon
                icon={showPassword ? faEyeSlash : faEye}
                className="cursor-pointer text-gray-500"
                onClick={toggleShowPassword}
              />
            </div>
          </div>

          <div className="h-6">
            {error && <div className="text-red-500 text-sm">{error}</div>}
            {resendEmailStatus && <div className="text-green-500 text-sm">{resendEmailStatus}</div>}
          </div>

          <button
            type="submit"
            className="flex w-full justify-center rounded-md bg-[#101010] px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-[#484848] focus:outline-none"
          >
            Sign in
          </button>
        </form>
        <button
          onClick={resendConfirmationEmail}
          className="mt-4 flex w-full justify-center rounded-md bg-[#101010] px-3 py-1.5 text-sm font-semibold leading-6 text-white shadow-sm hover:bg-[#484848] focus:outline-none"
        >
          Resend Confirmation Email
        </button>
        <p className="mt-10 text-center text-sm text-gray-500">
          {"Don't have an account? "}
          <a
            href="./signup"
            className="font-semibold leading-6 text-[#101010] hover:text-[#484848]"
          >
            Sign up
          </a>
        </p>
      </div>
    </div>
  );
}

export default SignIn;

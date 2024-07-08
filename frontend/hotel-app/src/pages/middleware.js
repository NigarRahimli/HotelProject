// middleware.js
import { NextResponse } from 'next/server';

export function middleware(req) {
  const { cookies } = req;
  const accessToken = cookies['accessToken'];

  if (!accessToken) {
    const url = req.nextUrl.clone();
    url.pathname = '/signin';
    url.search = `?redirect=${req.nextUrl.pathname}`; // Add redirect query parameter
    return NextResponse.redirect(url);
  }

  return NextResponse.next();
}

export const config = {
  matcher: ['/favorites', '/rates', '/profile'], // Add all the protected paths here
};

import { useEffect } from 'react';
import { useAuth } from '../components/AuthProvider';
import { useRouter } from 'next/router';

const Rates = () => {
  const { isAuthenticated, isLoading } = useAuth();
  const router = useRouter();

  useEffect(() => {
    if (!isLoading) {
      console.log('Is Authenticated:', isAuthenticated); // Debug log
      if (!isAuthenticated) {
        router.push('/signin?redirect=/rates');
      }
    }
  }, [isAuthenticated, isLoading, router]);

  if (isLoading) {
    return <p>Loading...</p>; // Or any loading indicator
  }

  return (
    <div>
      <h1>Rates Page</h1>
      {/* Render rates content */}
    </div>
  );
};

export default Rates;

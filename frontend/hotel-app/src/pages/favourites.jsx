import { useEffect } from "react";
import { useAuth } from "../components/AuthProvider";
import { useRouter } from "next/router";

const Favorites = () => {
  const { isAuthenticated, isLoading } = useAuth();
  const router = useRouter();

  useEffect(() => {
    if (!isLoading) {
      console.log("Is Authenticated:", isAuthenticated); // Debug log
      if (!isAuthenticated) {
        router.push("/signin?redirect=/favourites");
      }
    }
  }, [isAuthenticated, isLoading, router]);

  if (isLoading) {
    return <p>Loading...</p>; // Or any loading indicator
  }

  return (
    <div>
      <h1>Favorites Page</h1>
      {/* Render favorites content */}
    </div>
  );
};

export default Favorites;

import '@/styles/globals.css';
import '@fontsource/montserrat';
import {AuthProvider} from '../components/AuthProvider';



function App({ Component, pageProps }) {
  return (
    <AuthProvider>
    <Component {...pageProps} />
  </AuthProvider>
);
}

export default App;

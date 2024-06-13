import Footer from '@/components/layout/Footer';
import Header from '@/components/layout/Header';
import React from 'react';


const Layout = ({ children }) => {
    return (
        <div className="layout">
            <Header />
            <main>{children}</main>
            <Footer />
        </div>
    );
};

export default Layout;

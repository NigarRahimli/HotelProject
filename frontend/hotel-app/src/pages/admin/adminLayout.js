import Navbar from '@/components/Admin/navbar';
import Sidebar from '@/components/Admin/sidebar';
import React from 'react';


const AdminLayout = ({ children }) => {
  return (
    <div className="flex h-full ">
        <div class="absolute w-full h-[280px] bg-blue-500 dark:hidden min-h-75"></div>
      <Sidebar />
      <div className="relative h-full max-h-screen transition-all duration-200 ease-in-out xl:ml-68 rounded-xl">
       <div className='ml-[280px]'> <Navbar /></div> 
        <main className="w-full pl-[305px] mx-auto">
        
          {children}
        </main>
      </div>
    </div>
  );
};

export default AdminLayout;

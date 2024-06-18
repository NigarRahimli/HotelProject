import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTv, faUser, faCopy, faCollection,faArrowRightToBracket  } from '@fortawesome/free-solid-svg-icons';
import Link from 'next/link';

const Sidebar = () => {
  return (
    <aside className="fixed inset-y-0 flex-wrap items-center justify-between block w-64 p-0 my-4 overflow-y-auto bg-white border-0 shadow-xl dark:shadow-none dark:bg-slate-850 max-w-64 z-990 xl:ml-6 rounded-2xl xl:left-0 xl:translate-x-0">
      <div className="h-19">
        <Link href="/admin/dashboard" className="block px-8 py-6 m-0 text-sm text-slate-700">
          <img src="/images/logo.png" className="h-8 max-h-8 mb-3" alt="main_logo" />
          <span className="ml-1 font-semibold">Dashboard</span>
        </Link>
      </div>
      <hr className="h-px mt-0 bg-gradient-to-r from-transparent via-black/40 to-transparent dark:via-white dark:to-transparent" />
      <div className="items-center block w-auto max-h-screen overflow-auto grow basis-full">
        <ul className="flex flex-col pl-0 mb-0">
          <li className="mt-0.5 w-full">
            <Link href="/admin/dashboard" className="py-2.7 bg-blue-500/13 text-sm my-0 mx-2 flex items-center rounded-lg px-4 font-semibold text-slate-700">
              <div className="mr-2 flex h-8 w-8 items-center justify-center rounded-lg bg-center stroke-0 text-center xl:p-2.5">
                <FontAwesomeIcon icon={faTv} className="text-blue-500" />
              </div>
              <span className="ml-1">Dashboard</span>
            </Link>
          </li>
  
          <li className="w-full mt-4">
            <h6 className="pl-6 ml-2 text-xs font-bold uppercase opacity-60">Account pages</h6>
          </li>
          <li className="mt-0.5 w-full">
            <Link href="/admin/profile" className="py-2.7 text-sm my-0 mx-2 flex items-center rounded-lg px-4 font-semibold text-slate-700">
              <div className="mr-2 flex h-8 w-8 items-center justify-center rounded-lg bg-center stroke-0 text-center xl:p-2.5">
                <FontAwesomeIcon icon={faUser} className="text-slate-700" />
              </div>
              <span className="ml-1">Profile</span>
            </Link>
          </li>
          <li className="mt-0.5 w-full">
            <Link href="/admin/signin" className="py-2.7 text-sm my-0 mx-2 flex items-center rounded-lg px-4 font-semibold text-slate-700">
              <div className="mr-2 flex h-8 w-8 items-center justify-center rounded-lg bg-center stroke-0 text-center xl:p-2.5">
              <FontAwesomeIcon icon={faArrowRightToBracket} className="text-orange-500" />
              </div>
              <span className="ml-1">Sign In</span>
            </Link>
          </li>
          <li className="mt-0.5 w-full">
            <Link href="/admin/signup" className="py-2.7 text-sm my-0 mx-2 flex items-center rounded-lg px-4 font-semibold text-slate-700">
              <div className="mr-2 flex h-8 w-8 items-center justify-center rounded-lg bg-center stroke-0 text-center xl:p-2.5">
                <FontAwesomeIcon icon={faArrowRightToBracket} className="text-cyan-500" />
              </div>
              <span className="ml-1">Sign Up</span>
            </Link>
          </li>
        </ul>
      </div>
    </aside>
  );
};

export default Sidebar;

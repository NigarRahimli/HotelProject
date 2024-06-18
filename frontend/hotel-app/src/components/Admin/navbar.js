import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faSearch, faUser, faCog, faBell } from '@fortawesome/free-solid-svg-icons';

const Navbar = () => {
  return (
    <nav className="  sm:w-[620px] md:w-[458px] lg:w-[724px] xl:w-[940px] flex justify-between items-center px-0 py-2 mx-6 ">
      <div className="flex items-center  w-[940px] justify-between">
        <nav>
          <h6 className="mb-0 font-bold text-white capitalize">Dashboard</h6>
        </nav>
        <div className="flex items-center justify-between">
          <div className="flex items-center justify-between  ">
            <div className="relative flex flex-wrap items-stretch w-full transition-all rounded-lg ease">
              <span className="text-sm   ease leading-5.6 absolute z-50 -ml-px flex h-full items-center whitespace-nowrap rounded-lg rounded-tr-none rounded-br-none border border-r-0 border-transparent bg-transparent py-2 px-2.5 text-center font-normal text-slate-500 transition-all">
                <FontAwesomeIcon icon={faSearch} />
              </span>
              <input type="text" className="pl-9 text-sm focus:shadow-primary-outline  rounded-lg border border-solid border-gray-300 dark:bg-slate-850 dark:text-white bg-white bg-clip-padding py-2 pr-3 text-gray-700 transition-all placeholder:text-gray-500 focus:border-blue-500 focus:outline-none focus:transition-shadow" placeholder="Type here..." />
            </div>
          </div>
          <ul className="flex flex-row justify-between pl-0 mb-0 list-none md-max:w-full">
            <li className="flex items-center ml-[30px]">
              <a href="/admin/signin" className="block px-0 py-2 text-sm font-semibold text-white">
                <FontAwesomeIcon icon={faUser} className="sm:mr-1" />
                <span className="hidden sm:inline">Sign In</span>
              </a>
            </li>
            <li className="flex items-center pl-4 xl:hidden">
              <a href="#" className="block p-0 text-white transition-all text-sm ease-nav-brand">
                <div className="w-4.5 overflow-hidden">
                  <FontAwesomeIcon icon={faBell} />
                </div>
              </a>
            </li>
            <li className="flex items-center px-4">
              <a href="javascript:;" className="p-0 text-white transition-all text-sm ease-nav-brand">
                <FontAwesomeIcon icon={faCog} />
              </a>
            </li>
            <li className="relative flex items-center pr-2">
              <p className="hidden transform-dropdown-show"></p>
              <a href="javascript:;" className="block p-0 text-white transition-all text-sm ease-nav-brand" dropdown-trigger aria-expanded="false">
                <FontAwesomeIcon icon={faBell} />
              </a>
            
            </li>
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;

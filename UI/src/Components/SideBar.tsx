
import MoreVertical from '~icons/ph/bell-simple-fill'
import ChevronLast from '~icons/ph/list-light';
import ChevronFirst from '~icons/ph/x';
import { useContext, createContext, useState, ReactNode } from "react";

// Define the context type
interface SidebarContextType {
    expanded: boolean;
}

// Create the context
const SidebarContext = createContext<SidebarContextType | null>(null);

// Define props for the Sidebar component
interface SidebarProps {
    children: ReactNode;
}

export default function Sidebar({ children }: SidebarProps) {
    const [expanded, setExpanded] = useState(true);

    return (
        <aside className="h-screen">
            <nav className="h-full flex flex-col bg-white  border-r shadow-sm">
                <div className="p-4 pb-2 flex justify-between items-center">
                    
                    <button
                        onClick={() => setExpanded((curr) => !curr)}
                        className="p-1.5  bg-white rounded-none"
                    >
                        {expanded ? <ChevronFirst  color='black' /> : <ChevronLast color='black' />}
                    </button>
                </div>

                <SidebarContext.Provider value={{ expanded }}>
                    <ul className="flex-1 px-3">{children}</ul>
                </SidebarContext.Provider>

                <div className="border-t flex p-3">
                    <img
                        src="https://ui-avatars.com/api/?background=c7d2fe&color=3730a3&bold=true"
                        alt=""
                        className="w-10 h-10 rounded-md"
                    />
                    <div
                        className={`
              flex justify-between items-center
              overflow-hidden transition-all ${expanded ? "w-52 ml-3" : "w-0"}
          `}
                    >
                        <div className="leading-4">
                            <h4 className="font-semibold">John Doe</h4>
                            <span className="text-xs text-gray-600">johndoe@gmail.com</span>
                        </div>
                        <MoreVertical scale={20} />
                    </div>
                </div>
            </nav>
        </aside>
    );
}

// Define props for the SidebarItem component
interface SidebarItemProps {
    icon: ReactNode;
    text: string;
    active?: boolean;
    alert?: boolean;
}

export function SidebarItem({ icon, text, active, alert }: SidebarItemProps) {
    const { expanded } = useContext(SidebarContext) as SidebarContextType;

    return (
        <li
            className={`
        relative flex  items-center py-2 px-3 my-4
        font-medium md:text-lg rounded-md cursor-pointer
        transition-colors group
        ${active
                    ? "bg-gradient-to-tr from-indigo-200  to-indigo-100 text-indigo-800"
                    : "hover:bg-indigo-50 text-gray-600"
                }
    `}
        >
            {icon}
            <span
                className={`overflow-hidden transition-all ${expanded ? "w-52 ml-3" : "w-0"
                    }`}
            >
                {text}
            </span>
            {alert && (
                <div
                    className={`absolute right-2 w-2 h-2 rounded bg-indigo-400 ${expanded ? "" : "top-2"
                        }`}
                />
            )}

            {!expanded && (
                <div
                    className={`
          absolute left-full rounded-md px-2 py-1 ml-6
          bg-indigo-100 text-indigo-800 text-sm
          invisible opacity-20 -translate-x-3 transition-all
          group-hover:visible group-hover:opacity-100 group-hover:translate-x-0
      `}
                >
                    {text}
                </div>
            )}
        </li>
    );
}
import { useState } from 'react'
import Home from '~icons/ph/house-line'
import Settings from '~icons/ph/gear'
import Bell from '~icons/ph/bell-simple-light'
import './App.css'
import Sidebar, { SidebarItem } from './Components/SideBar'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <div className="min-h-screen justify-between w-dvw flex">

        <Sidebar>
          <SidebarItem icon={<Home scale={2} />} text="Home" active />
          <SidebarItem icon={<Settings scale={20} />} text="Settings" />
          <SidebarItem icon={<Bell scale={20} />} text="Notifications" alert />
        </Sidebar>

        <div className=" w-full bg-amber-50 flex-1 p-4">

        </div>
      </div>

    </>
  )
}

export default App

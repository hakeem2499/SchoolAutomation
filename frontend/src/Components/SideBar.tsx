import clsx from 'clsx';
import React from 'react'

type Props = {
    showPopup: boolean;
    className?: string;
    children: React.ReactNode;
    as?: React.ElementType;
    onClose?: () => void;
}

const SideBar: React.FC<Props> = ({
    as: Comp = "aside",
    className,
    children,
    showPopup,
    onClose,
    ...restProps
}) => {
    if(!showPopup) return null;
  return (
    <Comp 
    className={clsx('inset-0 fixed w-full z-100 bg-primary h-full', className)}
    {...restProps}>
        <div className="mx-auto flex h-full p-4 gap-8 mt-10   flex-col items-center">
			{children}
		</div>
    </Comp>
  )
}

export default SideBar;
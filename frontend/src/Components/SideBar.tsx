import clsx from 'clsx';
import React from 'react';

type Props = {
    showPopup: boolean;
    className?: string;
    children: React.ReactNode;
    as?: React.ElementType;
    onClose?: () => void;
};

const SideBar: React.FC<Props> = ({
    as: Comp = "aside",
    className,
    children,
    showPopup,
    onClose,
    ...restProps
}) => {
    if (!showPopup) return null;

    return (
        <Comp
            className={clsx(
                'fixed inset-0 w-full h-full flex z-50 transition-transform duration-300 ease-in-out motion-reduce:transition-none',
                className
            )}
            {...restProps}
        >
            {/* Overlay (optional) */}
            <div
                className="fixed inset-0 bg-black bg-opacity-50"
                onClick={onClose}
                aria-hidden="true"
            />

            {/* Sidebar Content */}
            <div className="relative flex items-start flex-col w-full max-w-md bg-black h-full overflow-y-auto">
                {/* Close Button */}
                {onClose && (
                    <button
                        onClick={onClose}
                        className="  text-xl py-8 text-brand mx-auto focus:outline-none"
                        aria-label="Close sidebar"
                    >
                        &larr; <span>Main menu</span>
                    </button>
                )}

                {/* Children with Custom Top Spacing */}
                <div  className="flex p-4 flex-col flex-1 w-full gap-8 mt-10"> {/* Use pt-20 for more spacing */}
                    {children}
                </div>
            </div>
        </Comp>
    );
};

export default SideBar;
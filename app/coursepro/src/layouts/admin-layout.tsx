import { Outlet, useLocation, useNavigate } from "react-router";
import { Layout, Menu, theme } from 'antd';
import React from "react";
import './styles/admin-layout.scss'
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faBookOpen, faGauge, faUserAlt } from "@fortawesome/free-solid-svg-icons";

const { Header, Content, Sider } = Layout;

const navItems = [ 
    {key: 'dashboard', value: faGauge, label: 'Dashboard' }, 
    {key: 'courses', value: faBookOpen, label: 'Courses' },
    {key: 'students', value: faUserAlt, label: 'Students' },
    ].map(
  (icon) => ({
    key: icon.key,
    icon: React.createElement(FontAwesomeIcon, { icon: icon.value }),
    label: icon.label,
  }),
);


function AdminLayout() {

    const {
        token: { colorBgContainer },
      } = theme.useToken();

      let selectedNavItem: string[] = ['dashboard']
      const location = useLocation();
    
      let currentactive = navItems.find(c=> location.pathname.includes(c.label.toLowerCase()))
    
      if(currentactive){
        selectedNavItem = currentactive.key.endsWith("admin")? ['dashboard'] : currentactive? [currentactive.key] : [];
      }
      const navigate = useNavigate();

      function onMenuSelect(data: any){
        console.log(data)
        navigate(data.key == 'dashboard'? '': data.key)
    
      }
      
    return (  
    <>
    <Layout className="layout">
        <Header style={{ padding: 0, background: colorBgContainer }} />
      
      <Layout>
      <Sider
        breakpoint="lg"
        collapsible 
        onBreakpoint={(broken) => {
          console.log(broken);
        }}
        onCollapse={(collapsed, type) => {
          console.log(collapsed, type);
        }}
        className="side-nav"
      >
        <Menu theme="dark" 
            mode="inline" 
            defaultSelectedKeys={selectedNavItem} 
            onSelect={onMenuSelect}
            items={navItems} />
      </Sider>
        <Content>
          <div className="content-section">
            <Outlet></Outlet>
          </div>
        </Content>
      </Layout>
    </Layout>
        
    </>
    );
}

export default AdminLayout;
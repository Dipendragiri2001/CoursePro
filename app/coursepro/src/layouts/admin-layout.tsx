import { Outlet, useLocation, useNavigate } from "react-router";
import {
  Avatar,
  Dropdown,
  Flex,
  Layout,
  Menu,
  theme,
  type MenuProps,
} from "antd";
import React, { useState } from "react";
import "./styles/admin-layout.scss";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { UserOutlined } from "@ant-design/icons";

import {
  faBookOpen,
  faGauge,
  faUserAlt,
} from "@fortawesome/free-solid-svg-icons";

const { Header, Content, Sider } = Layout;

const navItems = [
  { key: "dashboard", value: faGauge, label: "Dashboard" },
  { key: "courses", value: faBookOpen, label: "Courses" },
  { key: "students", value: faUserAlt, label: "Students" },
].map((icon) => ({
  key: icon.key,
  icon: React.createElement(FontAwesomeIcon, { icon: icon.value }),
  label: icon.label,
}));

function AdminLayout() {
  function logout() {
    localStorage.removeItem("authResult");
    navigate("/auth/login");
  }
  
  const {
    token: { colorBgContainer },
  } = theme.useToken();

  let selectedNavItem: string[] = ["dashboard"];
  const location = useLocation();

  let currentactive = navItems.find((c) =>
    location.pathname.includes(c.label.toLowerCase())
  );

  if (currentactive) {
    selectedNavItem = currentactive.key.endsWith("admin")
      ? ["dashboard"]
      : currentactive
      ? [currentactive.key]
      : [];
  }
  const navigate = useNavigate();

  function onMenuSelect(data: any) {
    console.log(data);
    navigate(data.key == "dashboard" ? "" : data.key);
  }
  let [isMenuCollapsed, setMenuCollapsed] = useState(false);

  const items: MenuProps['items'] = [
    {
      key: '1',
      danger: true,
      label: 'Log out',
      onClick: logout
    },
  ];

  return (
    <>
      <Layout className="layout">
        <Header className="header" style={{background: colorBgContainer}}>
          <h1>Logo</h1>
          <Flex gap="middle" justify="end" align="end">
            <Dropdown menu={{items}}>
              <Avatar size="large" icon={<UserOutlined />} style={{cursor: "pointer"}}/>
            </Dropdown>
          </Flex>
        </Header>

        <Layout>
          <Sider
            breakpoint="lg"
            collapsible
            collapsed={isMenuCollapsed}
            onBreakpoint={(broken) => {
              console.log(broken);
            }}
            onCollapse={(collapsed, type) => {
              if ((type = "clickTrigger")) setMenuCollapsed(!isMenuCollapsed);
              console.log(collapsed, type);
            }}
            className="side-nav"
          >
            <Menu
              theme="dark"
              mode="inline"
              defaultSelectedKeys={selectedNavItem}
              onSelect={onMenuSelect}
              items={navItems}
            />
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


<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="./css/sett.css">
</head>
<body>

    <header><?php 
        $title = 'Settings';
        $page = 'sett'; 
        include './navbar.php'; ?></header>
<div class="settings">
    <nav>
        <ul>
            <li><a href="#profile-settings" class="tab-link">Profile</a></li>
            <li><a href="#password-settings" class="tab-link">Password Settings</a></li>
            <li><a href="#delete-account" class="tab-link">Delete Account</a></li>
        </ul>
    </nav>
    <main>
        <section id="profile-settings" class="tab-content">
            <h2>Profile</h2>
            <form action="settapi.php" method="POST">
                <input type="hidden" name="action" value="update-profile">

                <label>Username:</label>
                <span id="userr"><?php echo $_SESSION['user']['username']; ?></span>
                <input type="text" class="del" id="username" name="username">
                <a href="#" id="user-change">Change</a><br>

                <label>Email:</label>
                <span id="emaill"><?php echo $_SESSION['user']['email']; ?></span>
                <input type="email" class="del" id="email" name="email">
                <a href="#" id="email-change">Change</a><br>
                
            <button class="buttt" type="submit">Update Profile</button>
            </form>
            
        </section>

        <section id="password-settings" class="tab-content">
            <h2>Password Settings</h2>
            <form action="settapi.php" method="POST">
                <input type="hidden" name="action" value="change-password">

                <label>New Password:</label><br>
                <input type="password" class="del" id="new-password" name="new-password" required><br>

                <label>Confirm New Password:</label><br>
                <input type="password" class="del" id="confirm-password" name="confirm-password" required><br>

                <button class="buttt" type="submit">Change Password</button>
            </form>
        </section>

        <section id="delete-account" class="tab-content">
            <h2>Delete account</h2>
            <form action="settapi.php" method="POST">
                <input type="hidden" name="action" value="delete-account">

                <label>Are you sure, you want to delete your account?</label><br>
                <input type="text" class="del" id="delete-account" name="delete-username" placeholder="Type your username..." required><br>

                <button class="buttt" type="submit">Delete account</button>
            </form>
        </section>
    </main>
</div>
    <footer>
        <p>Copyright &copy; 2024 Game name, Designed by Suba Dávid, Basa Dávid</p>
    </footer>
    <script src="sett.js"></script>
</body>
</html>
